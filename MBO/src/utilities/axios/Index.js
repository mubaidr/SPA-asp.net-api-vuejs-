import axios from 'axios'
import store from 'utilities/store'
import router from 'utilities/router'

axios.interceptors.request.use(cfg => {
  if (store.getters.isAuthenticated) {
    const auth = store.getters.getAuth
    cfg.headers.Authorization = `${auth.token_type} ${auth.access_token}`
  }
  return cfg
}, err => {
  return Promise.reject(err)
})

axios.interceptors.response.use(res => res, err => {
  switch (err.response.status) {
    case 401:
      store.commit('removeAuthentication')
      router.push({
        path: '/signin',
        query: {
          redirect: router.app._route.fullPath
        }
      })
      break
    default:
      return Promise.reject(err)
  }
})

export default axios
