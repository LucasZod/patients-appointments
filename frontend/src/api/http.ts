const BASE_URL = import.meta.env.VITE_API_URL ?? 'http://localhost:5134/api'

export class HttpError extends Error {
  constructor(
    public readonly status: number,
    message: string,
  ) {
    super(message)
    this.name = 'HttpError'
  }
}

const request = async <T>(path: string, options?: RequestInit): Promise<T> => {
  const response = await fetch(`${BASE_URL}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...options?.headers,
    },
    ...options,
  })

  if (!response.ok) {
    const fallback = `HTTP ${response.status}`
    const payload = await response.json().catch(() => null)

    let message = fallback
    if (payload?.error) {
      message = payload.error
    } else if (payload?.errors) {
      const first = Object.values(payload.errors as Record<string, string[]>).flat()[0]
      message = first ?? payload.title ?? fallback
    } else if (payload?.title) {
      message = payload.title
    }

    throw new HttpError(response.status, message)
  }

  if (response.status === 204) return undefined as T

  return response.json()
}

export const http = {
  get: <T>(path: string) => request<T>(path),
  post: <T>(path: string, body?: unknown) =>
    request<T>(path, { method: 'POST', body: body ? JSON.stringify(body) : undefined }),
  patch: <T>(path: string, body?: unknown) =>
    request<T>(path, { method: 'PATCH', body: body ? JSON.stringify(body) : undefined }),
}
