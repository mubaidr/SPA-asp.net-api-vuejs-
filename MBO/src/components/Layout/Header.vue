<template>
  <div>
    <md-whiteframe class="header-custom" md-tag="md-toolbar" md-elevation="3">
      <div class="md-toolbar-container">
      </div>
      <div class="md-toolbar-container">
        <md-button @click.native="toggleLeftSidenav">
          <md-tooltip md-direction="top">Menu</md-tooltip>
          <md-icon>menu</md-icon>
        </md-button>
        <span style="flex: 1;">
          <h2 class="md-title">MBO</h2>
        </span>
        <!--TODO Display notifications here!-->
        <md-speed-dial class="md-fab-bottom-right add-task" v-show="isAuthenticated" md-open="click" md-direction="left">
          <md-button class="md-fab" md-fab-trigger>
            <md-tooltip md-direction="top">New</md-tooltip>
            <md-icon md-icon-morph="">close</md-icon>
            <md-icon>add</md-icon>
          </md-button>
          <md-button class="md-fab md-accent md-mini md-clean" @click.native="redirect('/tasks/create')">
            <md-tooltip md-direction="top">Assigment</md-tooltip>
            <md-icon>assignment</md-icon>
          </md-button>
          <md-button class="md-fab md-accent md-mini md-clean" @click.native="redirect('/messages?folder=compose')">
            <md-tooltip md-direction="top">Message</md-tooltip>
            <md-icon>chat</md-icon>
          </md-button>
        </md-speed-dial>
      </div>
    </md-whiteframe>
    <md-sidenav class="md-left md-fixed" ref="leftSidenav">
      <md-whiteframe class="md-default" md-tag="md-toolbar" md-elevation="0">
        <div class="md-toolbar-container"></div>
        <div class="md-toolbar-container">
          <h2 class="md-title" v-show="!isAuthenticated">My Account</h2>
          <h2 class="md-title" v-show="isAuthenticated">{{userInfo.Email}}</h2>
          <span style="flex: 1;"></span>
        </div>
        <md-list v-show="!isAuthenticated">
          <md-subheader>
            <span>Account</span>
          </md-subheader>
          <md-list-item>
            <md-icon>person_outline</md-icon>
            <span>
              <router-link to="/signin">Sign In</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>person_add</md-icon>
            <span>
              <router-link to="/signup">Register</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>settings_backup_restore</md-icon>
            <span>
              <router-link to="/recover">Recover</router-link>
            </span>
          </md-list-item>
          <md-subheader>
            <span>Support</span>
          </md-subheader>
          <md-list-item>
            <md-icon>phone</md-icon>
            <span>
              <router-link to="/contact">Contact</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>info</md-icon>
            <span>
              <router-link to="/about">About</router-link>
            </span>
          </md-list-item>
        </md-list>
        <md-list v-show="isAuthenticated">
          <md-subheader>
            <span>Account</span>
          </md-subheader>
          <md-list-item>
            <md-icon>dashboard</md-icon>
            <span>
              <router-link class="md-clean" to="/dashboard">Dashboard</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>person</md-icon>
            <span>
              <router-link to="/profile">Profile</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>subdirectory_arrow_left</md-icon>
            <span>
              <a href="#" @click="openDialog('signout')">Sign Out</a>
            </span>
          </md-list-item>
          <md-subheader>
            <span>Tasks</span>
          </md-subheader>
          <md-list-item>
            <md-icon>add</md-icon>
            <span>
              <router-link to="/tasks/create">New</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment_return</md-icon>
            <span>
              <router-link :to="{path: '/tasks', query: {sub: 'Assigned'}}">Assigned to me</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment</md-icon>
            <span>
              <router-link :to="{path: '/tasks', query: {sub: 'Created'}}">Created by me</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment_turned_in</md-icon>
            <span>
              <router-link :to="{path: '/tasks', query: {sub: 'Completed'}}">Completed</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>delete</md-icon>
            <span>
              <router-link to="/tasks/trash">Trash</router-link>
            </span>
          </md-list-item>
          <md-subheader>
            <span>Messages</span>
          </md-subheader>
          <md-list-item>
            <md-icon>add</md-icon>
            <span>
              <router-link :to="{path: '/messages', query: {folder: 'compose'}}">New</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>mail</md-icon>
            <span>
              <router-link :to="{path: '/messages', query: {folder: 'inbox'}}">Inbox</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>send</md-icon>
            <span>
              <router-link :to="{path: '/messages', query: {folder: 'sent'}}">Sent</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>delete</md-icon>
            <span>
              <router-link :to="{path: '/messages', query: {folder: 'trash'}}">Trash</router-link>
            </span>
          </md-list-item>
          <md-subheader>
            <span>Support</span>
          </md-subheader>
          <md-list-item>
            <md-icon>phone</md-icon>
            <span>
              <router-link to="/contact">Contact</router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>info</md-icon>
            <span>
              <router-link to="/about">About</router-link>
            </span>
          </md-list-item>
        </md-list>
      </md-whiteframe>
    </md-sidenav>
    <md-dialog-confirm md-title="Are you sure to Sign Out?" md-content=" " md-ok-text="Sign Out" md-cancel-text="Cancel" ref="signout" @close="onClose">
    </md-dialog-confirm>
  </div>
</template>
<script>
import {
  signout
} from 'services/account'

export default {
  name: 'app-header',
  watch: {
    '$route' () {
      window.setTimeout(this.$refs.leftSidenav.close, 500)
    }
  },
  computed: {
    isAuthenticated () {
      return this.$store.getters.isAuthenticated
    },
    userInfo () {
      return this.$store.getters.getUserInfo || {}
    }
  },
  methods: {
    openDialog (ref) {
      this.$refs[ref].open()
    },
    onClose (type) {
      if (type === 'ok') {
        signout().then(res => {
          this.$router.push({
            path: '/signout'
          })
        }).catch(err => {
          console.log(err)
        })
      }
    },
    toggleLeftSidenav () {
      this.$refs.leftSidenav.toggle()
    },
    redirect (path) {
      this.$router.push({
        path
      })
    }
  },
  mounted () { }
}

</script>
<style scoped="">
.header-custom {
  margin-bottom: 1%;
}

.add-task {
  bottom: -20%!important;
  right: 0.75%!important;
}
</style>
