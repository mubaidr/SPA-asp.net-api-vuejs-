import api from 'api'
import axios from 'axios'
import qs from 'qs'

export default {

  signup (obj) {
    return axios.post(`${api.url}/api/Account/Register`, obj)
  },

  signin (obj) {
    // eslint-disable-next-line camelcase
    obj.grant_type = 'password'
    return axios.post(`${api.url}/Token`, qs.stringify(obj))
  },

  signout () {
    return axios.post(`${api.url}/api/Account/Logout`)
  },

  getUserInfo () {
    return axios.get(`${api.url}/api/Account/UserInfo`)
  },

  getUserList () {
    return axios.get(`${api.url}/api/Account/Users`)
  }

}
