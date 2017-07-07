import api from 'api'
import axios from 'axios'
import qs from 'qs'

export default {
  signup (obj) {
    return new Promise((resolve, reject) => {
      axios.post(`${api.url}/api/Account/Register`, obj).then((res) => {
        console.log(res)
        resolve(res)
      }).catch((err) => {
        console.log(err)
        reject(err)
      })
    })
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
