export {
  create,
  edit,
  remove,
  list
};
import api from 'api';
import axios from 'utilities/axios';
import qs from 'qs';

const path = "/api/MainTasks/";

function create(obj) {
  return axios.post(api.url + path, obj);
}

function edit(obj) {
  return axios.post(api.url + path, obj);
}

function remove(obj) {
  return axios.post(api.url + path, obj);
}

function list(obj) {
  return axios.post(api.url + path, obj);
}
