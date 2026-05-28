const BASE_URL = import.meta.env.VITE_API_URL ?? 'http://localhost:5134/api'

const request = async <T>(path: string, options?: RequestInit): Promise<T> => {
  const response = await fetch(`${BASE_URL}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...options?.headers,
    },
    ...options,
  })

  if (!response.ok) {
    const fallback = { error: `HTTP ${response.status}` }
    const payload = await response.json().catch(() => fallback)
    throw new Error(payload.error ?? fallback.error)
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
