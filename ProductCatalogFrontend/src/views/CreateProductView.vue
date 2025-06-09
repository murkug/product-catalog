<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { getProductTypes, getColors, createProduct } from '@/services/api';

const router = useRouter();

// Form state
const name = ref('');
const selectedProductType = ref('');
const selectedColors = ref([]);

// Data for form controls
const availableProductTypes = ref([]);
const availableColors = ref([]);

// UI state
const isLoading = ref(false);
const error = ref(null);
const isFetchingInitialData = ref(true);

// Fetch data for dropdowns and checkboxes on component mount
onMounted(async () => {
  try {
    [availableProductTypes.value, availableColors.value] = await Promise.all([
      getProductTypes(),
      getColors(),
    ]);
  } catch (err) {
    error.value = 'Failed to load form data. Please try refreshing the page.';
  } finally {
    isFetchingInitialData.value = false;
  }
});

const handleSubmit = async () => {
  if (!name.value || !selectedProductType.value || selectedColors.value.length === 0) {
    error.value = 'Please fill out all fields.';
    return;
  }

  isLoading.value = true;
  error.value = null;

  const productData = {
    name: name.value,
    productType: parseInt(selectedProductType.value),
    colors: selectedColors.value,
  };

  try {
    await createProduct(productData);
    // On success, navigate to the products list
    router.push('/');
  } catch (err) {
    error.value = err.message || 'An unknown error occurred.';
  } finally {
    isLoading.value = false;
  }
};
</script>

<template>
  <div class="create-product">
    <h1>Create a New Product</h1>

    <div v-if="isFetchingInitialData" class="loading">Loading form...</div>

    <form v-else @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="name">Product Name</label>
        <input type="text" id="name" v-model="name" required />
      </div>

      <div class="form-group">
        <label for="product-type">Product Type</label>
        <select id="product-type" v-model="selectedProductType" required>
          <option disabled value="">Please select one</option>
          <option v-for="type in availableProductTypes" :key="type.id" :value="type.id">
            {{ type.name }}
          </option>
        </select>
      </div>

      <div class="form-group">
        <label>Available Colors</label>
        <div class="checkbox-group">
          <div v-for="color in availableColors" :key="color.id" class="checkbox-item">
            <input type="checkbox" :id="'color-' + color.id" :value="color.id" v-model="selectedColors" />
            <label :for="'color-' + color.id">{{ color.name }}</label>
          </div>
        </div>
      </div>

      <div v-if="error" class="error-message">{{ error }}</div>
      
      <button type="submit" :disabled="isLoading">
        {{ isLoading ? 'Creating...' : 'Create Product' }}
      </button>
    </form>
  </div>
</template>

<style scoped>

form {
  max-width: 600px;
  margin: 0 auto;
}

h1 {
  text-align: center;
  margin-bottom: 2rem;
}

.form-group {
  margin-bottom: 1.5rem;
}

label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: bold;
}

input[type="text"],
select {
  width: 100%;
  padding: 0.8rem;
  border: 1px solid var(--color-border);
  border-radius: 4px;
  background-color: var(--color-background);
  color: var(--color-text);
  font-size: 1rem;
}

.checkbox-group {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(120px, 1fr));
  gap: 1rem;
  padding: 1rem;
  border: 1px solid var(--color-border);
  border-radius: 4px;
}

.checkbox-item {
  display: flex;
  align-items: center;
}

.checkbox-item input {
  margin-right: 0.5rem;
}

button {
  width: 100%;
  padding: 1rem;
  font-size: 1.1rem;
  color: #fff;
  background-color: hsla(160, 100%, 37%, 1);
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.2s;
}

button:hover {
  background-color: hsla(160, 100%, 30%, 1);
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.error-message {
  color: #d83a3a;
  background-color: rgba(216, 58, 58, 0.1);
  padding: 1rem;
  border-radius: 4px;
  margin-bottom: 1rem;
  text-align: center;
}
</style>