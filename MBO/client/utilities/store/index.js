import Vue from 'vue'
import Vuex from 'vuex'
import router from 'utilities/router'

Vue.use(Vuex)

const state = {
  page: {
    loading: false,
    alert: false,
    success: true,
    message: '',
    details: []
  }
}

const mutations = {
  isLoading(state) {
    state.page.loading = true;
  },
  isNotLoading(state) {
    state.page.loading = false;
  },
  clearState(state) {
    state.page.loading = false;
    state.page.alert = false;
    state.page.success = true;
    state.page.message = '';
    state.page.details.length = 0;
  },
  setState(state, obj) {
    if (obj.err) {
      state.page.success = false;
      state.page.alert = true;

      var err = obj.err;
      state.page.details.length = 0;
      if (!err.response || !err.response.data) {
        state.page.message = "Unable to contact server!";
      } else if (typeof err.response.data.error === 'string') {
        state.page.message = err.response.data.error;
        state.page.details.push(err.response.data.error_description);
      } else if (typeof err.response.data.ModelState === 'object') {
        //TODO fix this
        var modelState = err.response.data.ModelState[""];
        state.page.message = err.response.data.Message;

        for (var i = 0; i < modelState.length; i++) {
          state.page.details.push(modelState[i]);
        }
      } else {
        state.page.message = "Something went wrong!";
      }
    }
  }
}

const actions = {}

const getters = {
  isLoading: function (state) {
    return state.page.loading;
  },
  page: function (state) {
    return state.page;
  }
}

const store = new Vuex.Store({
  state,
  mutations,
  actions,
  getters
})

export default store
