export {
  getDashboard
}

import api from 'api'
import axios from 'utilities/axios'

const path = '/api/Dashboard'

function getDashboard () {
  return axios.get(api.url + path)
}
