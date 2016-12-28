import Vue from 'vue'
import Router from 'vue-router'
//Anonymous
import Home from 'components/Home/Home'
import About from 'components/Home/About'
import Contact from 'components/Home/Contact'
//Error
import Error_404 from 'components/Error/Error_404'
import Error_500 from 'components/Error/Error_500'
//Account
import Signin from 'components/Account/Signin'
import Signup from 'components/Account/Signup'
import Profile from 'components/Account/Profile'
import Signout from 'components/Account/Signout'
import Recover from 'components/Account/Recover'
//Data
import Dashboard from 'components/Management/Dashboard/'
import Settings from 'components/Management/Settings/'
import Search from 'components/Management/Search/'

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
    path: '/welcome',
    name: 'welcome',
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/about',
    name: 'about',
    component: About
  }, {
    path: '/contact',
    name: 'contact',
    component: Contact
  }, {
    path: '/signup',
    name: 'signup',
    component: Signup
  }, {
    path: '/signin',
    name: 'signin',
    component: Signin
  }, {
    path: '/signout',
    name: 'signout',
    component: Signin
  }, {
    path: '/recover',
    name: 'recover',
    component: Recover
  }, {
    path: '/dashboard',
    name: 'dashboard',
    component: Dashboard,
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/settings',
    name: 'settings',
    component: Settings,
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/profile',
    name: 'profile',
    component: Profile,
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/search',
    name: 'search',
    component: Search,
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/404',
    name: '404',
    component: Error_404
  }, {
    path: '/error',
    name: 'error',
    component: Error_500
  }, {
    path: '*',
    name: 'any',
    redirect: '/404'
  }]
})

/*
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
*/
