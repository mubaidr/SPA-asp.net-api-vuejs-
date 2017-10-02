<template>
  <div>
    <md-whiteframe class="header-custom md-large" md-tag="md-toolbar" md-elevation="3">
      <div class="md-toolbar-container"></div>
      <div class="md-toolbar-container">
        <md-button @click.native="toggleSidebar">
          <md-tooltip md-direction="top">Menu</md-tooltip>
          <md-icon>menu</md-icon>
        </md-button>
        <span style="flex: 1;">
          <h2 class="md-title">MBO</h2>
        </span>
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
    <side-bar :open="sideBarOpen"></side-bar>
  </div>
</template>
<script>
  import sidebar from './Sidebar'
  import { mapGetters, mapActions } from 'vuex'

  export default {
    name: 'app-header',
    components: { 'side-bar': sidebar },
    data () {
      return {
        sideBarOpen: false
      }
    },
    watch: {
      'isAuthenticated' () {
        this.$router.push({
          path: '/'
        })
      }
    },
    computed: {
      ...mapGetters(['isAuthenticated'])
    },
    methods: {
      toggleSidebar () {
        this.sideBarOpen = !this.sideBarOpen
      },
      redirect (path) {
        this.$router.push({
          path
        })
      }
    }
  }

</script>
<style scoped>
  .add-task {
    bottom: -20%!important;
    right: 1.5%!important;
  }
</style>
