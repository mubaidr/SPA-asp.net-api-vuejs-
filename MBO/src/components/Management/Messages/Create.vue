<template>
  <div>
    <!--<md-whiteframe md-tag="section" class="full-width">-->
      <md-toolbar class="md-transparent md-dense">
        <md-button class="md-icon-button md-accent" @click.native="$router.push({ path: '/messages/inbox' })">
          <md-tooltip md-direction="top">Inbox</md-tooltip>
          <md-icon>arrow_back</md-icon>
        </md-button>
        <span>Send Message</span>
        <span style="flex: 1"></span>
        <md-button class="md-accent" @click.native="$router.push({path: '/messages/sent'})">
          <md-tooltip md-direction="top">Sent</md-tooltip>
          <md-icon>assignment</md-icon>
          Tasks
        </md-button>
        <md-button class="md-accent" @click.native="$router.push({path: '/messages/archived'})">
          <md-tooltip md-direction="top">Archived</md-tooltip>
          <md-icon>assignment</md-icon>
          Tasks
        </md-button>
        <md-button class="md-accent" @click.native="$router.push({path: '/dashboard'})">
          <md-tooltip md-direction="top">Dashboard</md-tooltip>
          <md-icon>dashboard</md-icon>
          Dashboard
        </md-button>
      </md-toolbar>
    <!--</md-whiteframe>-->
    <br/>
    <md-layout>
      <md-layout md-hide-small></md-layout>
      <md-layout>
        <message-create></message-create>
      </md-layout>
      <md-layout md-hide-small>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
  import messageCreate from 'components/Management/Messages/_partial_create.vue'

  export default {
    components: {
      'message-create': messageCreate
    }
  }

</script>
