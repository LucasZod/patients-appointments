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
        {
          path: 'orders/new',
          name: 'orders-new',
          component: () => import('@/features/ServiceOrders/NewServiceOrder/index.vue'),
        },
        {
          path: 'queue',
          name: 'queue',
          component: () => import('@/features/Queue/QueueView/index.vue'),
        },
        {
          path: 'orders/:id',
          name: 'order-detail',
          component: () => import('@/features/ServiceOrders/ServiceOrderDetail/index.vue'),
        },
        {
          path: 'orders/:id/samples/record',
          name: 'samples-record',
          component: () => import('@/features/Samples/RecordSamples/index.vue'),
        },
        {
          path: 'orders/:id/samples/review',
          name: 'samples-review',
          component: () => import('@/features/Samples/SampleReview/index.vue'),
        },
        { path: 'reports', name: 'reports', component: PlaceholderView },
      ],
    },
  ],
})

export default router
