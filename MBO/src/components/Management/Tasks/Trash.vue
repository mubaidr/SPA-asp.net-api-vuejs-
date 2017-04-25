<template>
  <div>
    <!--<md-whiteframe md-tag="section" class="full-width">-->
    <md-toolbar class="md-transparent md-dense">
      <md-button class="md-icon-button" md-hide-small @click.native="$router.push({ path: '/tasks' })">
        <md-tooltip md-direction="top">Tasks</md-tooltip>
        <md-icon class="md-accent">arrow_back</md-icon>
      </md-button>
      <span>Archived Tasks</span>
      <span style="flex: 1"></span>
      <md-button class="md-accent" @click.native="$router.push({path: '/tasks/'})">
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
    <br>
    <pagination :lastpage="Tasks.Trash.lastPage" :loading="Tasks.Trash.loading" :count="Tasks.Trash.count" :view-menu="false" :compact="true" @refresh="loadTrash"></pagination>
    <md-layout md-gutter="">
      <md-layout v-show="activeView == 'List'" md-hide-small></md-layout>
      <md-layout md-gutter="">
        <transition-group class="min-height no-padding full-width simple-list" name="list-out" tag="ul" v-if="Tasks.Trash.content.length">
          <li class="list-out-item" :class="{'full-width' : activeView == 'List'}" v-for="Task in Tasks.Trash.content" v-bind:key="Task.MainTaskID">
            <task-card-trash @remove-task-item="removeTaskItem" :Task="Task" v-if="activeView == 'Card'"></task-card-trash>
            <task-list-item-trash @remove-task-item="removeTaskItem" :Task="Task" v-if="activeView == 'List'"></task-list-item-trash>
          </li>
        </transition-group>
        <div class="flex-vertical min-height full-width" v-else>
          <div class="no-content">
            <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br>
            <span v-if="Tasks.Trash.loading">Loading...</span>
            <span v-else="">Awww... Nothing here!</span>
            <span v-show="Tasks.Trash.error">An error occured while trying to fetch data.</span>
          </div>
        </div>
      </md-layout>
      <md-layout v-show="activeView == 'List'" md-hide-small></md-layout>
    </md-layout>
    <md-snackbar md-position="bottom center" ref="snackbar" md-duration="60000">
      <span>Unable to fetch data!<br> If the problem persists please contact support.</span>
      <md-button class="md-accent" @click.native="retry">Retry</md-button>
    </md-snackbar>
  </div>
</template>
<script>
  import taskCardTrash from 'components/_custom/task-card-trash.vue'
  import taskListItemTrash from 'components/_custom/task-list-item-trash.vue'
  import pagination from 'components/_custom/pagination.vue'
  import {
    listTrash
  } from 'services/tasks'

  export default {
    name: 'task-list-trash',
    components: {
      'task-card-trash': taskCardTrash,
      'task-list-item-trash': taskListItemTrash,
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
        this.Tasks.Trash.loading = false
      },
      'failAlert' (val) {
        if (val) {
          this.$refs.snackbar.open()
        } else {
          this.$refs.snackbar.close()
        }
      }
    },
    computed: {
      settings () {
        return this.$store.getters.getSettings
      },
      activeView () {
        for (var i = 0; i < this.settings.taskView.view.length; i++) {
          if (this.settings.taskView.view[i].enabled) {
            return this.settings.taskView.view[i].name
          }
        }
      }
    },
    methods: {
      retry () {
        this.$refs.snackbar.close()
        window.setTimeout(() => {
          this.loadTrash()
        }, 500)
      },
      removeTaskItem (obj) {
        const id = obj.id
        const ts = this.Tasks.Trash.content

        for (let i = 0; i < ts.length; i++) {
          if (ts[i].MainTaskID === id) {
            this.Tasks.Trash.content.splice(i, 1)
            break
          }
        }
        this.retry()
      },
      loadTrash (paging) {
        this.Tasks.Trash.loading = true
        listTrash(paging).then(res => {
          this.Tasks.Trash.content = res.data.mainTask
          this.Tasks.Trash.lastPage = res.data.lastPage
          this.Tasks.Trash.count = res.data.count
        }).catch(err => {
          this.failAlert = true
          console.log(err)
        }).then(() => {
          this.Tasks.Trash.loading = false
        })
      }
    },
    mounted () {
      window.setTimeout(() => {
        this.loadTrash()
      }, 250)
    }
  }

</script>
<style scoped="">
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
  
  .simple-list {
    list-style: none;
    padding: 0;
    margin: 0;
  }

</style>
