import axios from 'axios'
import store from 'utilities/store'

axios.interceptors.request.use(config => {
  if (store.getters.isAuhtenticated) {
    const auth = store.getters.getAuth
    config.headers.Authorization = `${auth.token_type} ${auth.access_token}`
  }

  return config
}, error => Promise.reject(error))

// TODO redirect to login if auth failed or expired
axios.interceptors.response.use(response => response, error => Promise.reject(error))

export default axios
