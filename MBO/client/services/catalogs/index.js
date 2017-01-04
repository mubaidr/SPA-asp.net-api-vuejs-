export {
  getCategories,
  getStatus
};

import api from 'api';
import axios from 'utilities/axios';

function getCategories() {
  //TOFIX axios undefined
  return axios.get(api.url + '/api/Categories');
};

function getStatus() {
  return axios.get(api.url + '/api/Status');
};
