import api from 'api'
// eslint-disable-next-line
import axios from 'utilities/axios'

const path = '/api/Dashboard'

export default {
  get () {
    return axios.get(api.url + path)
  }
}
