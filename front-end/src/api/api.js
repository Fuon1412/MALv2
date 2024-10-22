import axios from 'axios'

const apiUrl = import.meta.env.VITE_APP_API_URL
console.log(apiUrl)

const api = axios.create({
  baseURL: apiUrl
})

export const registerUser = (userData) => {
  return api.post('/Account/register', userData)
}
export const loginUser = (userData) => {
  return api.post('/Account/login', userData)
}
