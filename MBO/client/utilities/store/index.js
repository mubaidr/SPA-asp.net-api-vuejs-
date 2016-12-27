import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const state = {
  page: {
    loading: false, //is request loading?
    valid: true, //Page state
    response: {}
  }
}

const mutations = {
  toggleValidity(state) {
    state.page.valid = !state.page.valid;
  },
  isValid(state) {
    state.page.valid = true;
  },
  isNotValid(state) {
    state.page.valid = false;
  },
  toggleLoading(state) {
    state.page.loading = !state.page.loading;
  },
  isLoading(state) {
    state.page.loading = true;
  },
  isNotLoading(state) {
    state.page.loading = false;
  },
  setResponse(state, response) {
    state.page.response = response;
  },
  setState(state, page) {
    if (typeof page.loading === 'undefined') {
      page.loading = state.page.loading;
    }
    if (typeof page.valid === 'undefined') {
      page.valid = state.page.valid;
    }
    if (typeof page.response === 'undefined') {
      page.response = state.page.response;
    }
    state.page = page;
  }
}

const actions = {}

//TODO add getter for error object (list and heading)
const getters = {}

const store = new Vuex.Store({
  state,
  mutations,
  actions,
  getters
})

export default store
