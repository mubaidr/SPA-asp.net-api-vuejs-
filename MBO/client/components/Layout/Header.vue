<template>
  <div>
    <md-whiteframe md-tag="md-toolbar" md-elevation="3">
      <div class="md-toolbar-container">
      </div>
      <div class="md-toolbar-container">
        <md-button @click="toggleLeftSidenav">
          <md-icon>menu</md-icon>
        </md-button>
        <span style="flex: 1;">
          <md-layout md-hide-xsmall>
            <h2 class="md-title">Get Busy with Management By Objectives!</h2>
          </md-layout>
        </span>
        <md-menu md-direction="bottom left" v-show="!isAuthenticated">
          <md-button md-menu-trigger>
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
        <md-menu md-direction="bottom left" v-show="isAuthenticated">
          <md-button md-menu-trigger>
            <md-icon>account_circle</md-icon>
            {{userinfo.Email}}
          </md-button>
          <md-menu-content>
            <md-menu-item>
              <router-link to="/dashboard">Dashboard</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/profile">Profile</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/contact">Contact Us</router-link>
            </md-menu-item>
            <md-menu-item>
              <router-link to="/about">About</router-link>
            </md-menu-item>
            <md-menu-item>
              <span @click="openDialog('signout')">Sign Out</span>
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
    <md-dialog-confirm md-title="Are you sure to Sign Out?" md-content=" " md-ok-text="Sign Out" md-cancel-text="Cancel" ref="signout"
      @close="onClose">
      </md-dialog-confirm>
  </div>
</template>
<script>
  import {
    signout
  } from 'services/account'

  const vm = {
    computed: {
      isAuthenticated: function () {
        return this.$store.getters.isAuhtenticated;
      },
      userinfo: function () {
        return this.$store.getters.getUserInfo || {};
      }
    },
    methods: {
      openDialog(ref) {
        this.$refs[ref].open();
      },
      onClose(type) {
        const _self = this;

        if (type === 'ok') {
          signout().then(function (res) {

            _self.$router.push({
              path: '/signout'
            });

          }).catch(err => {});
        }
      },
      toggleLeftSidenav() {
        this.$refs.leftSidenav.toggle();
      }
    }
  }
  export default vm;

</script>
