import { createRouter, createWebHistory } from 'vue-router'

import DefaultLayout from '@/shared/layouts/DefaultLayout.vue'
import PlaceholderView from '@/shared/ui/PlaceholderView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      component: DefaultLayout,
      children: [
        { path: '', redirect: '/orders/new' },
        { path: 'orders/new', name: 'orders-new', component: PlaceholderView },
        { path: 'queue', name: 'queue', component: PlaceholderView },
        { path: 'orders/:id', name: 'order-detail', component: PlaceholderView },
        {
          path: 'orders/:id/samples/record',
          name: 'samples-record',
          component: PlaceholderView,
        },
        {
          path: 'orders/:id/samples/review',
          name: 'samples-review',
          component: PlaceholderView,
        },
        { path: 'reports', name: 'reports', component: PlaceholderView },
      ],
    },
  ],
})

export default router
