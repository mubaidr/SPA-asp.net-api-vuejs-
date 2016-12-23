<template>
  <div>
    <md-whiteframe md-tag="md-toolbar" md-elevation="3" class="md-large">
      <div class="md-toolbar-container">
      </div>
      <div class="md-toolbar-container">
        <md-button @click="toggleLeftSidenav">
          <md-icon>menu</md-icon>
          <!--Menu-->
        </md-button>
        <md-layout md-hide-medium>
          <h2 class="md-title">Get Busy with Management By Objectives!</h2>
        </md-layout>
        <span style="flex: 1;"></span>
        <md-menu md-direction="bottom left" v-show="!status.authenticated">
          <md-button md-menu-trigger>
            <!--<md-icon>account_circle</md-icon>-->
            My Account
            <md-icon>more_vert</md-icon>
          </md-button>
          <md-menu-content>
            <md-menu-item>
              <router-link to="/signin">Sign In</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/signup">Register</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/about">About</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/contact">Contact Us</router-link>
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
              <router-link to="/settings">Settings</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/about">About</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/contact">Contact Us</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/signout">Sign Out</router-link>
            </md-menu-item>
          </md-menu-content>
        </md-menu>
      </div>
    </md-whiteframe>
    <md-sidenav class="md-left" ref="leftSidenav">
      <md-whiteframe md-tag="md-toolbar" md-elevation="0" class="md-default">
        <div class="md-toolbar-container">
        </div>
        <div class="md-toolbar-container">
        </div>
      </md-whiteframe>
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
          authenticated: false
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
