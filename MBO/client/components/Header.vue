<template>
  <div>
    <md-whiteframe md-tag="md-toolbar" md-elevation="3" class="md-medium">
      <div class="md-toolbar-container">
        <md-button class="md-icon-button" @click="toggleLeftSidenav">
          <md-icon>menu</md-icon>
        </md-button>
        <span style="flex: 1;" md-theme="red">
        <md-button @click="home">
          Home
        </md-button>
      </span>
        <md-button @click="search" class="md-icon-button">
          <md-icon>search</md-icon>
        </md-button>
        <md-menu md-direction="bottom left" v-show="!status.authenticated">
          <md-button md-menu-trigger>
            <md-icon>account_circle</md-icon>
            My Account
          </md-button>
          <md-menu-content>
            <md-menu-item>
              <router-link to="/signin">Sign in</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/signup">Register</router-link>
            </md-menu-item>
          </md-menu-content>
        </md-menu>
        <md-menu md-direction="bottom left" v-show="status.authenticated">
          <md-button md-menu-trigger>
            <md-icon>account_circle</md-icon>
            {{user.email}}
          </md-button>
          <md-menu-content>
            <md-menu-item>
              <router-link to="/profile">Profile</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/signout">Sign out</router-link>
            </md-menu-item>
          </md-menu-content>
        </md-menu>
      </div>
      <!--<div class="md-toolbar-container">
      </div>-->
    </md-whiteframe>
    <md-sidenav class="md-left" ref="leftSidenav">
      <md-toolbar class="md-large">
        <div class="md-toolbar-container">
          <h3 class="md-title">Sidenav content</h3>
        </div>
      </md-toolbar>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nisi cupiditate esse necessitatibus beatae nobis, deserunt
        ut est fugit, tempora deleniti, eligendi commodi doloribus. Nemo, assumenda possimus, impedit inventore perferendis
        iusto!
      </p>
    </md-sidenav>
  </div>
</template>
<script>
  import VueRouter from 'vue-router'
  export default {
    data: function () {
      return {
        status: {
          authenticated: true
        },
        user: {
          email: 'tester@test.com',
          avatar: '',
          notifications: 0
        }
      }
    },
    methods: {
      home: function () {
        this.$router.push('home');
      },
      search: function () {
        this.$router.push('search');
      },
      toggleLeftSidenav: function () {
        this.$refs.leftSidenav.toggle();
      }
    }
  }

</script>
