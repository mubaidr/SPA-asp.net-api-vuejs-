import Vue from 'vue'
import {
  sync
} from 'vuex-router-sync'
import App from 'components/App'
import router from 'utilities/router'
import store from 'utilities/store'
//import components from 'components/_custom'

sync(store, router)

const app = new Vue({
  router,
  store,
  ...App
})

export {
  app,
  router,
  store
}
