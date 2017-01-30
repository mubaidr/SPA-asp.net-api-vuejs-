<template>
  <div>
    <md-layout md-gutter="40">
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">Heading</md-subheader>
          <md-list-item>
            <md-icon>add</md-icon>
            <span>Subheading</span>
          </md-list-item>
        </md-list>
      </md-layout>
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">Heading</md-subheader>
          <md-list-item>
            <md-icon>add</md-icon>
            <span>Subheading</span>
          </md-list-item>
        </md-list>
      </md-layout>
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">Heading</md-subheader>
          <md-list-item>
            <md-icon>add</md-icon>
            <span>Subheading</span>
          </md-list-item>
        </md-list>
      </md-layout>
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">Heading</md-subheader>
          <md-list-item>
            <md-icon>add</md-icon>
            <span>Subheading</span>
          </md-list-item>
        </md-list>
      </md-layout>
    </md-layout>
    <br/>
    <md-layout md-gutter="40">
      <md-layout md-flex-xsmall="100" md-flex-small="70" md-flex-medium="66" md-flex-large="75" md-flex-xlarge="75">
        <span class="md-display-1">Graph Here</span>
      </md-layout>
      <md-layout md-flex-xsmall="100" md-flex-small="30" md-flex-medium="33" md-flex-large="25" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">My Tasks</md-subheader>
          <md-list-item>
            <md-icon>add</md-icon>
            <router-link to="/tasks/create">Create</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment_return</md-icon>
            <router-link :to="{path: '/tasks', query: {sub: 'Assigned'}}">Assigned to me</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment</md-icon>
            <router-link :to="{path: '/tasks', query: {sub: 'Created'}}">Created by me</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment_turned_in</md-icon>
            <router-link :to="{path: '/tasks', query: {sub: 'Completed'}}">Completed</router-link>
          </md-list-item>
          <md-list-item>
            <md-icon>delete</md-icon>
            <router-link to="/tasks/trash">Trash</router-link>
          </md-list-item>
          <md-subheader class="md-inset">Messages</md-subheader>
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
        </md-list>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
  export default {}

</script>
