export {
  create,
  edit,
  remove,
  restore,
  deleteTask,
  getDetails,
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

function getDetails(obj) {
  return axios.get(api.url + path, obj);
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

function listAssigned(obj) {
  return axios.get(api.url + path + '?type=assigned' + objToQuery(obj));
}

function listCreated(obj) {
  return axios.get(api.url + path + '?type=created' + objToQuery(obj));
}

function listCompleted(obj) {
  return axios.get(api.url + path + '?type=completed' + objToQuery(obj));
}

function listTrash(obj) {
  return axios.get(api.url + path + '?type=trash' + objToQuery(obj));
}

function objToQuery(obj) {
  let query = '';
  let val = '';
  for (const prop in obj) {
    val = obj[prop];
    if (val && (val.trim && val.trim() !== '') || val > 0) {
      query += '&' + prop + '=' + val;
    }
  }
  return query;
}
