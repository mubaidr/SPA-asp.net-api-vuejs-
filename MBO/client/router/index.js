import Vue from 'vue'
import Router from 'vue-router'
import Home from '../components/Home/Home.vue'
import About from '../components/Home/About.vue'
import Contact from '../components/Home/Contact.vue'
import Error_404 from '../components/Error/Error_404.vue'
import Error_500 from '../components/Error/Error_500.vue'
import Login from '../components/Account/Login.vue'
import Signup from '../components/Account/Signup.vue'
import Dashboard from '../components/Management/Dashboard.vue'

Vue.use(Router)

export default new Router({
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
})
