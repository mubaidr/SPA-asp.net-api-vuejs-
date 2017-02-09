<template>
  <div>
    <span class="md-display-1">Archived Tasks</span>
    <p>
      <router-link class="md-accent" :to="{path: '/tasks'}">
        View All Tasks
      </router-link>
    </p>
    <pagination :lastpage="Tasks.Trash.last_page" :loading="Tasks.Trash.loading" :count="Tasks.Trash.count" @refresh="loadTrash"></pagination>
    <!--Convert this section in to component-->
    <md-layout md-gutter>
      <div class="flex-vertical min-height full-width" v-show="!Tasks.Trash.content.length">
        <div class="no-content">
          <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br/>
          <span v-if="Tasks.Trash.loading">Loading...</span>
          <span v-else>Awww... Nothing here!</span>
          <span v-show="Tasks.Trash.error">An error occured while trying to fetch data.</span>
        </div>
      </div>
      <transition-group name="list-out" tag="ul" class="no-padding">
        <li class="list-out-item" v-for="Task in Tasks.Trash.content" v-bind:key="Task.MainTaskID">
          <task-card-trash @remove-task-item="removeTaskItem" :Task="Task"></task-card-trash>
        </li>
      </transition-group>
    </md-layout>
    <md-snackbar md-position="bottom center" ref="snackbar" md-duration="60000">
      <span>Unable to fetch data!<br/> If the problem persists please contact support.</span>
      <md-button class="md-accent" @click.native="retry">Retry</md-button>
    </md-snackbar>
  </div>
</template>
<script>
  import taskCardTrash from 'components/_custom/task-card-trash.vue';
  import pagination from 'components/_custom/pagination.vue';
  import {
    listTrash
  } from 'services/tasks';

  export default {
    name: 'task-list-trash',
    components: {
      'task-card-trash': taskCardTrash,
      'pagination': pagination
    },
    data: function () {
      return {
        Tasks: {
          Trash: {
            name: 'Assigned',
            icon: 'assignment_return',
            content: [],
            loading: true,
            last_page: 1,
            count: 0
          }
        },
        failAlert: false
      }
    },
    watch: {
      'Tasks.Trash.content': function () {
        this.$set(this.Tasks.Trash, 'loading', false);
      },
      'failAlert': function (val) {
        const _self = this;
        if (val) {
          _self.$refs.snackbar.open();
        } else {
          _self.$refs.snackbar.close();
        }
      }
    },
    methods: {
      retry: function () {
        const _self = this;
        _self.$refs.snackbar.close();
        window.setTimeout(function () {
          _self.loadTrash();
        }, 500);
      },
      removeTaskItem: function (obj) {
        const _self = this;
        var id = obj.id;
        var ts = _self.Tasks.Trash.content;

        for (var i = 0; i < ts.length; i++) {
          if (ts[i].MainTaskID == id) {
            _self.Tasks.Trash.content.splice(i, 1);
            break;
          }
        }
        this.retry();
      },
      loadTrash: function (paging) {
        const _self = this;
        _self.$set(_self.Tasks.Trash, 'loading', true);
        listTrash(paging).then(res => {
          _self.$set(_self.Tasks.Trash, 'content', res.data.mainTask);
          _self.$set(_self.Tasks.Trash, 'last_page', res.data.last_page);
          _self.$set(_self.Tasks.Trash, 'count', res.data.count);
        }).catch(err => {
          _self.$set(_self.Tasks.Trash, 'loading', false);
          _self.$set(_self, 'failAlert', true);
        });
      }
    },
    mounted: function () {
      const _self = this;

      window.setTimeout(function () {
        _self.loadTrash();
      }, 250);

    }
  }

</script>
<style scoped>
  .no-content {
    text-align: center;
  }
  
  .no-content span {
    font-size: 1.25em;
    opacity: 0.75;
  }
  
  .no-content i {
    margin-bottom: 10px;
  }

</style>
