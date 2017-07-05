import axios from 'axios'
import store from 'utilities/store'
import router from 'utilities/router'

axios.interceptors.request.use(config => {
  if (store.getters.isAuthenticated) {
    const auth = store.getters.getAuth
    config.headers.Authorization = `${auth.token_type} ${auth.access_token}`
  }
  return config
}, (error) => {
  Promise.reject(error)
})

axios.interceptors.response.use(response => response, error => {
  if (error.response.status === 401) {
    store.commit('removeAuthentication')
    router.push('/')
  } else {
    Promise.reject(error)
  }
})

export default axios