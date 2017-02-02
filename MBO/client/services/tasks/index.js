export {
  create,
  edit,
  remove,
  restore,
  deleteTask,
  listAll,
  listAssigned,
  listCreated,
  listCompleted,
  listTrash
};

import api from 'api';
import axios from 'utilities/axios';
import qs from 'qs';

const path = "/api/MainTasks";

function create(obj) {
  return axios.post(api.url + path, obj);
}

function edit(obj) {
  return axios.post(api.url + path, obj);
}

function remove(obj) {
  return axios.post(api.url + path + '/Remove/?id=' + obj.id, obj);
}

function restore(obj) {
  return axios.post(api.url + path + '/Restore/?id=' + obj.id, obj);
}

function deleteTask(obj) {
  return axios.post(api.url + path, obj);
}

function listAll() {
  return axios.get(api.url + path);
}

function listAssigned() {
  return axios.get(api.url + path + '?type=assigned');
}

function listCreated() {
  return axios.get(api.url + path + '?type=created');
}

function listCompleted() {
  return axios.get(api.url + path + '?type=completed');
}

function listTrash(obj) {
  return axios.get(api.url + path + '?type=trash&page=' + obj.page);
}
