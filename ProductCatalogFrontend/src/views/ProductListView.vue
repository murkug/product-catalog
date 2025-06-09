<script setup>
import { ref, onMounted } from 'vue';
import { getProducts } from '@/services/api';

const products = ref([]);
const isLoading = ref(true);
const error = ref(null);

onMounted(async () => {
  try {
    products.value = await getProducts();
  } catch (err) {
    error.value = err.message;
  } finally {
    isLoading.value = false;
  }
});
</script>

<template>
  <div class="products-list">
    <h1>Products</h1>
    <div v-if="isLoading" class="loading">Loading products...</div>
    <div v-if="error" class="error-message">{{ error }}</div>
    <table v-if="!isLoading && !error && products.length">
      <thead>
        <tr>
          <th>Product ID</th>
          <th>Product Name</th>
          <th>Product Type</th>
          <th>Colours</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="product in products" :key="product.id">
          <td>{{ product.id }}</td>
          <td>{{ product.name }}</td>
          <td>{{ product.productType }}</td>
          <td>{{ product.colours.join(', ') }}</td>
        </tr>
      </tbody>
    </table>
    <div v-if="!isLoading && !products.length" class="no-products">
      No products found. Why not <router-link to="/create">create one</router-link>?
    </div>
  </div>
</template>

<style scoped>

h1 {
  color: var(--color-heading);
  border-bottom: 1px solid var(--color-border);
  padding-bottom: 0.5rem;
  margin-bottom: 1rem;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  padding: 12px 15px;
  border: 1px solid var(--color-border);
  text-align: left;
}

thead {
  background-color: var(--color-background-soft);
}

th {
  font-weight: bold;
  color: var(--color-heading);
}

tbody tr:nth-of-type(even) {
  background-color: var(--color-background-mute);
}

tbody tr:hover {
  background-color: hsla(160, 100%, 37%, 0.2);
}

.loading, .error-message, .no-products {
  text-align: center;
  padding: 2rem;
  font-size: 1.2rem;
}

.error-message {
  color: #d83a3a;
}
</style>