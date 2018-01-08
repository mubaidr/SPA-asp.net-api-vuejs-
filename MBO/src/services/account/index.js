// eslint-disable-next-line
import axios from 'utilities/axios'
// eslint-disable-next-line
import qs from 'qs'

import api from 'api'

export default {
  signup (obj) {
    return axios.post(`${api.url}/api/Account/Register`, obj)
  },
  signin (obj) {
    // eslint-disable-next-line
    obj.grant_type = 'password'
    return axios.post(`${api.url}/Token`, qs.stringify(obj))
  },
  signout () {
    return axios.post(`${api.url}/api/Account/Logout`)
  },
  getUserInfo () {
    return axios.get(`${api.url}/api/Account/UserInfo`)
  },
  getUsers () {
    return axios.get(`${api.url}/api/Account/Users`)
  }
}
