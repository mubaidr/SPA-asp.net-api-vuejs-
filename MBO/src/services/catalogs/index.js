// eslint-disable-next-line
import axios from 'utilities/axios'
import api from 'api'

export default {
  getCategories () {
    return axios.get(`${api.url}/api/Categories`)
  },

  getStatus () {
    return axios.get(`${api.url}/api/Status`)
  }
}
