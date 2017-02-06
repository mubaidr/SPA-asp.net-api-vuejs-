<template>
  <div>
    <md-whiteframe md-tag="md-toolbar" md-elevation="3">
      <div class="md-toolbar-container">
      </div>
      <div class="md-toolbar-container">
        <md-button @click="toggleLeftSidenav">
          <md-tooltip md-direction="top">Menu</md-tooltip>
          <md-icon>menu</md-icon>
        </md-button>
        <span style="flex: 1;">
          <md-layout md-hide-xsmall>
            <h2 class="md-title">Get Busy with Management By Objectives!</h2>
          </md-layout>
        </span>
        <router-link v-show="isAuthenticated" tag="md-button" to="/tasks/create" class="md-fab md-fab-bottom-right add-task">
          <md-tooltip md-direction="top">Create new Task</md-tooltip>
          <md-icon>add</md-icon>
        </router-link>
      </div>
    </md-whiteframe>
    <md-sidenav class="md-left" ref="leftSidenav">
      <md-whiteframe md-tag="md-toolbar" md-elevation="0" class="md-default">
        <div class="md-toolbar-container"></div>
        <div class="md-toolbar-container">
          <h2 class="md-title" v-show="!isAuthenticated">My Account</h2>
          <h2 class="md-title" v-show="isAuthenticated">{{userinfo.Email}}</h2>
          <span style="flex: 1;"></span>
          <!--TODO Display notifications here!-->
          <!--<md-icon>notifications_active</md-icon>-->
        </div>
        <md-list v-show="!isAuthenticated">
          <md-subheader>
            <span>Account</span>
          </md-subheader>
          <md-list-item>
            <md-icon>person_outline</md-icon>
            <router-link to="/signin">Sign In</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>person_add</md-icon>
            <router-link to="/signup">Register</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>settings_backup_restore</md-icon>
            <router-link to="/recover">Recover</router-link>
          </md-list-item>
          <md-subheader>
            <span>Support</span>
          </md-subheader>
          <md-list-item>
            <md-icon>phone</md-icon>
            <router-link to="/contact">Contact</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>info</md-icon>
            <router-link to="/about">About</router-link>
          </md-list-item>
        </md-list>
        <md-list v-show="isAuthenticated">
          <md-subheader>
            <span>Account</span>
          </md-subheader>
          <md-list-item>
            <md-icon>dashboard</md-icon>
            <router-link to="/dashboard">Dashboard</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>person</md-icon>
            <router-link to="/profile">Profile</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>subdirectory_arrow_left</md-icon>
            <a @click="openDialog('signout')">Sign Out</a>
          </md-list-item>
          <md-subheader>
            <span>Tasks</span>
          </md-subheader>
          <md-list-item>
            <md-icon>assignment_return</md-icon>
            <router-link :to="{path: '/tasks'}">List</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>delete</md-icon>
            <router-link to="/tasks/trash">Trash</router-link>
          </md-list-item>
          <md-subheader>
            <span>Messages</span>
          </md-subheader>
          <md-list-item>
            <md-icon>add</md-icon> <span>Compose</span>
          </md-list-item>
          <md-list-item>
            <md-icon>mail</md-icon> <span>Inbox</span>
          </md-list-item>
          <md-list-item>
            <md-icon>send</md-icon> <span>Outbox</span>
          </md-list-item>
          <md-list-item>
            <md-icon>delete</md-icon> <span>Trash</span>
          </md-list-item>
          <md-subheader>
            <span>Support</span>
          </md-subheader>
          <md-list-item>
            <md-icon>phone</md-icon>
            <router-link to="/contact">Contact</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>info</md-icon>
            <router-link to="/about">About</router-link>
          </md-list-item>
        </md-list>
      </md-whiteframe>
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
<style scoped>
  .add-task {
    bottom: -25%!important;
    right: 8px!important;
  }

</style>
