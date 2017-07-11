import api from 'api'
import axios from 'utilities/axios'

const path = '/api/Dashboard'

export default {

  get () {
    return axios.get(api.url + path)
  }

}
