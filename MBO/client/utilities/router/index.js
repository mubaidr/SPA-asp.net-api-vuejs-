import Vue from 'vue';
import Router from 'vue-router';
import store from 'utilities/store';
//Anonymous
import Home from 'components/Home/Home';
import About from 'components/Home/About';
import Contact from 'components/Home/Contact';
//Error
import Error_ from 'components/Error/Error_';
import Error_404 from 'components/Error/Error_404';
import Error_500 from 'components/Error/Error_500';
//Account
import Signin from 'components/Account/Signin';
import Signup from 'components/Account/Signup';
import Profile from 'components/Account/Profile';
import Signout from 'components/Account/Signout';
import Recover from 'components/Account/Recover';
//Data
import Dashboard from 'components/Management/Dashboard.vue';
import Settings from 'components/Management/Settings.vue';
import Search from 'components/Management/Search.vue';
//Tasks
import Tasks from 'components/Management/Tasks/index.vue';
import TasksList from 'components/Management/Tasks/List.vue';
import TasksCreate from 'components/Management/Tasks/Create.vue';
import TasksEdit from 'components/Management/Tasks/Edit.vue';

Vue.use(Router);

const router = new Router({
  mode: 'history',
  root: '/',
  routes: [{
    path: '/',
    component: Home
  }, {
    path: '/home',
    redirect: '/'
  }, {
    path: '/welcome',
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/about',
    component: About
  }, {
    path: '/contact',
    component: Contact
  }, {
    path: '/signup',
    component: Signup
  }, {
    path: '/signin',
    component: Signin
  }, {
    path: '/signout'
  }, {
    path: '/recover',
    component: Recover
  }, {
    path: '/dashboard',
    component: Dashboard,
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/settings',
    component: Settings,
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/profile',
    component: Profile,
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/search',
    component: Search,
    meta: {
      requiresAuth: true
    }
  }, {
    path: '/error',
    component: Error_,
    children: [{
      path: '404',
      component: Error_404
    }, {
      path: '500',
      component: Error_500
    }]
  }, {
    //Tasks
    path: '/tasks',
    component: Tasks,
    meta: {
      requiresAuth: true
    },
    children: [{
      path: 'list',
      component: TasksList
    }, {
      path: 'create',
      component: TasksCreate
    }, {
      path: 'edit',
      component: TasksEdit
    }]
  }, {
    path: '*',
    redirect: '/error/404'
  }]
});

router.beforeEach((to, from, next) => {
  store.commit('clearState');
  var _message_content = to.params.message;
  if (_message_content) {
    var _message_type = to.params.type;

    store.commit('setMessage', {
      message: _message_content,
      type: _message_type || 'success'
    });

  }

  if (store.getters.isAuhtenticated) {
    var _not_valid = ['signin', 'signup', 'recover'];

    if (_not_valid.lastIndexOf(to.name) >= 0) {
      next({
        path: '/dashboard'
      });
    } else if (to.path === '/signout') {
      store.commit('removeAuthentication');
      next({
        path: '/home'
      });
    } else {
      next();
    }
  } else {
    if (to.matched.some(record => record.meta.requiresAuth)) {
      next({
        path: '/signin',
        query: {
          redirect: to.fullPath
        }
      });
    } else if (to.path === '/signout') {
      next({
        path: '/home'
      });
    } else {
      next();
    }
  }

});

export default router;
