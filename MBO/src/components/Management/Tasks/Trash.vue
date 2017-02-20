<template>
  <div>
    <md-whiteframe md-tag="section" class="full-width">
      <md-toolbar>
        <md-button md-hide-small class="md-icon-button" @click.native="$router.push({ path: '/tasks' })">
          <md-tooltip md-direction="top">Tasks</md-tooltip>
          <md-icon>arrow_back</md-icon>
        </md-button>
        <span class="md-headline">Archived Tasks</span>
        <span style="flex: 1"></span>
        <md-button @click.native="$router.push({path: '/dashboard'})">
          <md-tooltip md-direction="top">Dashboard</md-tooltip>
          Dashboard
        </md-button>
      </md-toolbar>      
    </md-whiteframe>
    <br/>
    <pagination :lastpage="Tasks.Trash.lastPage" :loading="Tasks.Trash.loading" :count="Tasks.Trash.count" @refresh="loadTrash"></pagination>
    <md-layout md-gutter>
      <transition-group name="list-out" tag="ul" class="no-padding">
        <li class="list-out-item" v-for="Task in Tasks.Trash.content" v-bind:key="Task.MainTaskID">
          <task-card-trash @remove-task-item="removeTaskItem" :Task="Task"></task-card-trash>
        </li>
      </transition-group>
      <div class="flex-vertical min-height full-width" v-show="!Tasks.Trash.content.length">
        <div class="no-content">
          <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br/>
          <span v-if="Tasks.Trash.loading">Loading...</span>
          <span v-else>Awww... Nothing here!</span>
          <span v-show="Tasks.Trash.error">An error occured while trying to fetch data.</span>
        </div>
      </div>
    </md-layout>
    <md-snackbar md-position="bottom center" ref="snackbar" md-duration="60000">
      <span>Unable to fetch data!<br/> If the problem persists please contact support.</span>
      <md-button class="md-accent" @click.native="retry">Retry</md-button>
    </md-snackbar>
  </div>
</template>
<script>
  import taskCardTrash from 'components/_custom/task-card-trash.vue'
  import pagination from 'components/_custom/pagination.vue'
  import {
    listTrash
  } from 'services/tasks'

  export default {
    name: 'task-list-trash',
    components: {
      'task-card-trash': taskCardTrash,
      'pagination': pagination
    },
    data () {
      return {
        Tasks: {
          Trash: {
            name: 'Assigned',
            icon: 'assignment_return',
            content: [],
            loading: true,
            lastPage: 1,
            count: 0
          }
        },
        failAlert: false
      }
    },
    watch: {
      'Tasks.Trash.content' () {
        this.$set(this.Tasks.Trash, 'loading', false)
      },
      'failAlert' (val) {
        const _self = this
        if (val) {
          _self.$refs.snackbar.open()
        } else {
          _self.$refs.snackbar.close()
        }
      }
    },
    methods: {
      retry () {
        const _self = this
        _self.$refs.snackbar.close()
        window.setTimeout(() => {
          _self.loadTrash()
        }, 500)
      },
      removeTaskItem (obj) {
        const _self = this
        const id = obj.id
        const ts = _self.Tasks.Trash.content

        for (let i = 0; i < ts.length; i++) {
          if (ts[i].MainTaskID === id) {
            _self.Tasks.Trash.content.splice(i, 1)
            break
          }
        }
        this.retry()
      },
      loadTrash (paging) {
        const _self = this
        _self.$set(_self.Tasks.Trash, 'loading', true)
        listTrash(paging).then(res => {
          _self.$set(_self.Tasks.Trash, 'content', res.data.mainTask)
          _self.$set(_self.Tasks.Trash, 'lastPage', res.data.lastPage)
          _self.$set(_self.Tasks.Trash, 'count', res.data.count)
        }).catch(err => {
          _self.$set(_self.Tasks.Trash, 'loading', false)
          _self.$set(_self, 'failAlert', true)
          console.log(err)
        })
      }
    },
    mounted () {
      const _self = this

      window.setTimeout(() => {
        _self.loadTrash()
      }, 250)
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
