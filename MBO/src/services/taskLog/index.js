import api from 'api'
import axios from 'axios'

export {
  addLog,
  getLog
}

function addLog (obj) {
  return axios.post(`${api.url}/api/Logs`, {
    params: obj
  })
};

function getLog (id) {
  return axios.get(`${api.url}/api/Logs?mainTaskID=${id}`)
};
