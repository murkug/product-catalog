const API_BASE_URL = 'https://localhost:7123/api/v1';

export async function getProducts() {
  const response = await fetch(`${API_BASE_URL}/products`);
  if (!response.ok) {
    throw new Error('Failed to fetch products');
  }
  return response.json();
}

export async function getProductTypes() {
  const response = await fetch(`${API_BASE_URL}/products/types`);
  if (!response.ok) {
    throw new Error('Failed to fetch product types');
  }
  return response.json();
}

export async function getColors() {
  const response = await fetch(`${API_BASE_URL}/products/colors`);
  if (!response.ok) {
    throw new Error('Failed to fetch colors');
  }
  return response.json();
}

export async function createProduct(productData) {
  const response = await fetch(`${API_BASE_URL}/products`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(productData),
  });

  if (!response.ok) {
    const errorData = await response.json();
    throw new Error(errorData.title || 'Failed to create product');
  }

  return response.json();
}