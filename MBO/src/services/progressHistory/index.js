import api from 'api'
// eslint-disable-next-line
import axios from 'utilities/axios'

export default {
  add () {
    return axios.post(`${api.url}/api/ProgressHistories`)
  },

  get (id) {
    // TODO get ProgressHistory based on task id
    return axios.get(`${api.url}/api/ProgressHistories?mainTaskID=${id}`)
  }
}
