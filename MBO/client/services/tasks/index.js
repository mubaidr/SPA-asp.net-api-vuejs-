export {
  create,
  edit,
  remove,
  list
};
import api from 'api';
import axios from 'utilities/axios';
import qs from 'qs';

function create(obj) {
  return axios.post(api.url + '/api/tasks/', obj);
}

function edit(obj) {
  return axios.post(api.url + '/api/tasks/', obj);
}

function remove(obj) {
  return axios.post(api.url + '/api/tasks/', obj);
}

function list(obj) {
  return axios.post(api.url + '/api/tasks/', obj);
}
