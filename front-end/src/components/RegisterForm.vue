<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-4">
        <h3 class="text-center">Register</h3>
        <form @submit.prevent="handleSubmit">
          <div class="form-group">
            <label for="username">Username</label>
            <input
              type="text"
              id="username"
              class="form-control"
              v-model="username"
              required
              placeholder="Enter username"
            />
          </div>
          <div class="form-group mt-3">
            <label for="email">Email</label>
            <input
              type="email"
              id="email"
              class="form-control"
              v-model="email"
              required
              placeholder="Enter email"
            />
          </div>
          <div class="form-group mt-3">
            <label for="password">Password</label>
            <input
              type="password"
              id="password"
              class="form-control"
              v-model="password"
              required
              placeholder="Enter password"
            />
          </div>
          <div class="form-group mt-3">
            <label for="confirmPassword">Confirm Password</label>
            <input
              type="password"
              id="confirmPassword"
              class="form-control"
              v-model="confirmPassword"
              required
              placeholder="Confirm password"
            />
          </div>
          <button type="submit" class="btn btn-primary w-100 mt-4">Register</button>
        </form>

        <!-- Error Message -->
        <div v-if="errorMessage" class="alert alert-danger mt-3">
          {{ errorMessage }}
        </div>

        <!-- Success Message -->
        <div v-if="successMessage" class="alert alert-success mt-3">
          {{ successMessage }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
import { registerUser } from '@/api/api';
import router from '@/router';

export default {
  name: 'RegisterForm',
  setup() {
    const username = ref('')
    const email = ref('')
    const password = ref('')
    const confirmPassword = ref('')
    const errorMessage = ref('')
    const successMessage = ref('')

    const handleSubmit = async () => {
      try {
        const userData = {
          Username: username.value,
          Email: email.value,
          Password: password.value,
          ConfirmPassword: confirmPassword.value
        }
        const result = await registerUser(userData);
        successMessage.value = 'Registration successful!\n Please login to continue.';
        errorMessage.value = '';

        username.value = '';
        password.value = '';
        email.value = '';
        confirmPassword.value = '';
        router.push('/login');
      } catch (error) {
        if (error.response && error.response.data && error.response.data.message) {
          errorMessage.value = error.response.data.message;
        } else {
          errorMessage.value = 'Register failed. Please check your credentials.';
        }
        successMessage.value = ''; 
      }
    }

    return {
      username,
      password,
      confirmPassword,
      errorMessage,
      successMessage,
      handleSubmit
    }
  }
}
</script>

<style scoped>
.container {
  max-width: 500px;
}
</style>
