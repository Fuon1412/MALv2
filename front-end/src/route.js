import { createRouter, createWebHistory } from 'vue-router';
import TheWelcome from './components/TheWelcome.vue';
import Login from './components/Login.vue';

// Define routes
const routes = [
    {
        path: '/',
        name: 'welcome',
        component: TheWelcome
    },
    {
        path: '/login',
        name: 'login',
        component: Login
    }
];

// Create the router instance
const router = createRouter({
    history: createWebHistory(import.meta.env.VITE_BASE_URL),
    routes
});

// Export the router
export default router;
