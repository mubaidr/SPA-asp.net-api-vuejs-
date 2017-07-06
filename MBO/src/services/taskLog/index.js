import api from 'api'
import axios from 'axios'

export default {

  addLog (obj) {
    return axios.post(`${api.url}/api/Logs`, {
      params: obj
    })
  },

  getLog (id) {
    return axios.get(`${api.url}/api/Logs?mainTaskID=${id}`)
  }

}
