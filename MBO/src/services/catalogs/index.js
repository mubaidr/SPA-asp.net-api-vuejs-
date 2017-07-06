import api from 'api'
import axios from 'axios'

export default {

  getCategories () {
    return axios.get(`${api.url}/api/Categories`)
  },

  getStatus () {
    return axios.get(`${api.url}/api/Status`)
  }

}
