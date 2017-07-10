import axios from 'axios'
import store from 'utilities/store'
import router from 'utilities/router'

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
  switch (error.response.status) {
    case 401:
      store.commit('removeAuthentication')
      router.push('/signin?redirect=' + router.app._route.path)
      break
    case 500:
      break
    default:
      return Promise.reject(error)
  }
})

export default axios
