<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    <nav>
      <RouterLink to="/">Products List</RouterLink>
      <RouterLink to="/create">Create a New Product</RouterLink>
    </nav>
  </header>

  <main>
    <RouterView />
  </main>
</template>

<style scoped>
header {
  margin-bottom: 2.5rem; /* Space between nav and page content */
}

nav {
  width: 100%;
  text-align: center;
  padding-bottom: 1.5rem;
  border-bottom: 1px solid var(--color-border);
}

nav a {
  font-weight: 500;
  padding: 0 1rem;
  font-size: 1.1rem;
}
</style>