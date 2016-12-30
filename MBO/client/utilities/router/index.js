import Vue from 'vue'
import Router from 'vue-router'
import store from 'utilities/store'

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

const router = new Router({
  mode: 'history',
  root: '/',
  routes: [{
    path: '/',
    name: 'root',
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

router.beforeEach((to, from, next) => {
  store.commit('clearState');
  var _message_content = to.params.message;
  if (_message_content) {
    var _message_type = to.params.type;

    store.commit('setMessage', {
      message: _message_content,
      type: _message_type || 'success'
    })

  }

  if (store.getters.isAuhtenticated) {
    var _not_valid = ['signin', 'signup', 'recover'];
    if (_not_valid.lastIndexOf(to.name) >= 0) {
      next({
        name: 'dashboard'
      })
    } else if (to.name === 'signout') {
      store.commit('removeAuthentication');
      next({
        name: 'home'
      })
    } else {
      next();
    }
  } else {
    if (to.matched.some(record => record.meta.requiresAuth)) {
      next({
        name: 'signin',
        query: {
          redirect: to.fullPath
        }
      })
    } else {
      next();
    }
  }

});

export default router
