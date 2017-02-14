export {
  addProgressHistory,
  getProgressHistory
};

import api from 'api';
import axios from 'axios';

function addProgressHistory() {
  return axios.post(api.url + '/api/ProgressHistories');
};

function getProgressHistory() {
  //TODO get ProgressHistory based on task id
  return axios.get(api.url + '/api/ProgressHistories');
};
