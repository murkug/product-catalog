import { createRouter, createWebHistory } from 'vue-router'
import ProductListView from '../views/ProductListView.vue'
import CreateProductView from '../views/CreateProductView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'products',
      component: ProductListView
    },
    {
      path: '/create',
      name: 'create-product',
      component: CreateProductView
    }
  ]
})

export default router
