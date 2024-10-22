<template>
    <div class="container mt-5">
      <div class="row justify-content-center">
        <div class="col-md-4">
          <h3 class="text-center">Login</h3>
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
            <button type="submit" class="btn btn-primary w-100 mt-4">Login</button>
          </form>
          <div v-if="errorMessage" class="alert alert-danger mt-3">
            {{ errorMessage }}
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import { ref } from 'vue'
  import { loginUser } from '@/api/api';
  import { useRouter } from 'vue-router';
  
  export default {
    setup() {
      const username = ref('');
      const password = ref('');
      const errorMessage = ref('');
      const router = useRouter(); 
  
      const handleSubmit = async () => {
        try {
          const userData = {
            Username: username.value,
            Password: password.value,
          };
          const result = await loginUser(userData);
          console.log(result);
          
          
          router.push('/'); 
  
        } catch (error) {
          if (error.response && error.response.data && error.response.data.message) {
            errorMessage.value = error.response.data.message; 
          } else {
            errorMessage.value = 'Login failed. Please check your credentials.';
          }
        }
      };
  
      return {
        username,
        password,
        errorMessage,
        handleSubmit,
      };
    }
  }
  </script>
  
  <style scoped>
  .container {
    max-width: 500px;
  }
  </style>
  