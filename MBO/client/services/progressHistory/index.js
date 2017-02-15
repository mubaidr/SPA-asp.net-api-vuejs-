export {
  addProgressHistory,
  getProgressHistory
};

import api from 'api';
import axios from 'axios';

function addProgressHistory(obj) {
  return axios.post(api.url + '/api/ProgressHistories');
};

function getProgressHistory(id) {
  //TODO get ProgressHistory based on task id
  return axios.get(api.url + '/api/ProgressHistories?mainTaskID=' + id);
};
