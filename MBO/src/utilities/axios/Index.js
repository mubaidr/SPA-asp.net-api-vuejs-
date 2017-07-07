import axios from 'axios'
import store from 'utilities/store'

axios.interceptors.request.use(config => {
  if (store.getters.isAuthenticated) {
    const auth = store.getters.getAuth
    config.headers.Authorization = `${auth.token_type} ${auth.access_token}`
  }
  return config
}, error => {
  return Promise.reject(error)
})

axios.interceptors.response.use(response => response, error => {
  return Promise.reject(error)
})

export default axios
