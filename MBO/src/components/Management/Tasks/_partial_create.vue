<template>
  <div class="flex-vertical min-height full-width">
    <md-card class="full-width">
      <md-card-header class="indigo">
        <md-card-header-text>
          <div>
            <div class="md-title">Get started</div>
            <span>New Task</span>
          </div>
        </md-card-header-text>
        <md-spinner class="md-accent" md-indeterminate v-show="Page.isLoading"></md-spinner>
      </md-card-header>
      <md-card-content>
        <md-input-container :class="{'md-input-invalid': errors.has('Title')}">
          <label>Title</label>
          <md-input name="Title" data-vv-name="Title" data-vv-rules="required|min:5" type="text" v-model="Task.Title" maxlength="50" v-validate></md-input>
          <span class="md-error">{{errors.first('Title')}}</span>
        </md-input-container>
        <!--<md-input-container>
            <label>Details <span>(optional)</span></label>
            <md-textarea v-model="Task.Description" maxlength="200" name="Description"></md-textarea>
          </md-input-container>-->
        <!--TODO Set default category in settings-->
        <md-input-container>
          <label for="Categories">Category</label>
          <md-select name="Categories" v-model="Task.CategoryID">
            <md-option v-for="category in Catalog.Categories" :value="category.CategoryID" :title="category.Description" :key="category.Id">{{category.Title}}</md-option>
          </md-select>
        </md-input-container>
        <md-input-container>
          <label for="Users">Assign To</label>
          <md-select name="Users" multiple v-model="Users">
            <md-option v-for="user in Catalog.Users" :value="user.Id" :title="user.UserName" :key="user.Id">{{user.UserName}}</md-option>
          </md-select>
        </md-input-container>
        <div>
          <label class="custom-label" for="DateDue">Target Date</label>
          <br>
          <date-picker :date="datepickerStartTime" :option="datepickerOption" :limit="datepickerLimit" orientation="landscape" autoOk="true"></date-picker>
        </div>
        <!--<app-message></app-message>-->
      </md-card-content>
      <md-card-actions>
        <!--<md-button md-hide-small class="md-accent" @click.native="$router.push({ path: '/tasks' })">
            <md-tooltip md-direction="top">Tasks</md-tooltip>
            Tasks
          </md-button>-->
        <md-button class="md-raised md-accent" id="btn-Submit" @click.native="formValidate" :disabled="Page.isLoading">Create New Task</md-button>
      </md-card-actions>
    </md-card>
  </div>
</template>
<script>
import {
  create
} from 'services/tasks'
import {
  getUsersList
} from 'services/account'
import {
  getCategories
} from 'services/catalogs'
import moment from 'moment'

export default {
  name: 'task-create',
  data () {
    return {
      Page: {
        isLoading: false
      },
      Task: {
        Title: '',
        Description: '',
        CategoryID: 1,
        DateDue: ''
      },
      Users: [],
      Catalog: {
        Categories: [],
        Users: []
      },
      datepickerStartTime: {
        time: ''
      },
      datepickerOption: {
        type: 'min',
        format: 'DD-MM-YYYY HH:mm A',
        inputStyle: {
          'width': '100%',
          'height': '32px',
          'font-size': '15px',
          'border': 'none',
          'border-bottom': '1px solid #e1e1e1',
          'box-shadow': 'none',
          'color': 'rgba(0,0,0,.87)',
          'font-weight': 'bold',
          'cursor': 'pointer'
        },
        week: ['Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa', 'Su'],
        month: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October',
          'November', 'December'
        ],
        autoOk: true
      },
      datepickerLimit: [{
        type: 'weekday',
        available: [1, 2, 3, 4, 5]
      }]
    }
  },
  methods: {
    formValidate (event) {
      event.preventDefault()

      if (this.datepickerStartTime.time) {
        this.Task.DateDue = moment(this.datepickerStartTime.time, 'DD-MM-YYYY HH:mm A').format('MM-DD-YYYY HH:mm A')
      }

      this.$validator.validateAll().then(success => {
        if (!success) return

        this.$store.commit('setState', {
          loading: true,
          alert: false
        })

        create({
          mainTask: this.Task,
          users: this.Users
        }).then(res => {
          this.$router.push({
            path: '/tasks'
          })
        }).catch(err => {
          this.setErrorDetails(err)
        })
      })
    }
  },
  created () {
    const now = moment().add(7, 'days').format('DD-MM-YYYY HH:mm A')
    this.Task.DateDue = now
    this.datepickerStartTime = now

    getCategories().then(res => {
      this.Catalog.Categories = res.data
    }).catch(err => {
      this.setErrorDetails(err)
    })

    getUsersList().then(res => {
      this.Catalog.Users = res.data
    }).catch(err => {
      this.setErrorDetails(err)
    })
  },
  mounted () { }
}

</script>
<style scoped="">
.compact-card .md-card-content,
.compact-card .md-card-actions {
  padding: 0;
}

.compact-card .md-card-actions {
  padding-top: 16px;
}

.compact-card .md-card {
  box-shadow: none;
}
</style>
