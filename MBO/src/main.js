//Main styles
import bootstrap_css from './assets/css/bootstrap.paper.css'
//import pace_css from './assets/css/pace.css'
import site_css from './assets/css/site.css'

//Get site script
//import bootstrap_js from './assets/script/'
//import pace_min_js from './assets/script/pace.min.js'
import site_js from './assets/script/site.js'

//Get vue
import Vue from 'vue'
import VueRouter from 'vue-router'
import VueResource from 'vue-resource'

//templates
import Home from './components/Home.vue'
import Login from './components/Login.vue'
import Signup from './components/Signup.vue'
import Dashboard from './components/Dashboard.vue'
import About from './components/About.vue'
import Contact from './components/Contact.vue'
import Error_500 from './components/Error_500.vue'
import Error_404 from './components/Error_404.vue'

//Services
import Auth from './services/auth'

//Configure Vue features
Vue.use(VueRouter);
Vue.use(VueResource);

//Setup routes
const router = new VueRouter({
  mode: 'history',
  root: '/',
  routes: [{
    path: '/',
    component: Home
  }, {
    path: '/home',
    name: 'home',
    redirect: '/'
  }, {
    path: '/about',
    component: About
  }, {
    path: '/contact',
    component: Contact
  }, {
    path: '/error',
    component: Error_500
  }, {
    path: '/signup',
    component: Signup
  }, {
    path: '/login',
    name: 'login',
    component: Login
  }, {
    path: '/dashboard',
    component: Dashboard,
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/404',
    component: Error_404
  }, {
    path: '*',
    redirect: '/404'
  }]
});

//Authentication
router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!auth.loggedIn()) {
      next({
        path: '/login',
        query: {
          redirect: to.fullPath
        }
      })
    } else {
      next();
    }
  } else {
    next();
  }
});

//Start Application
const app = new Vue({
  el: '#app',
  router: router
});
