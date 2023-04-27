/* eslint-disable */
<template>
  <v-content>
    <v-container
      fluid
      fill-height
    >
      <v-layout
        align-center
        justify-center
      >
        <div
          class="justify-center min-width"
          size="md"
        >
          <v-text-field
            v-model="container_id"
            clearable
            label="Container Id"
          ></v-text-field>
          <v-btn
            variant="elevated"
            @click="assign"
            >Assign</v-btn
          >
          <v-data-table
            v-model:items-per-page="itemsPerPage"
            :headers="headers"
            :items="items"
            item-value="name"
            class="elevation-1"
          >
            <template v-slot:item.outgoing_date="{ item }">
              <v-label>{{ dateFormat(item.value.OutgoingDateTime) }}</v-label>
            </template>
            <template v-slot:item.is_selected="{ item }">
              <v-checkbox-btn v-model="item.value.is_selected"></v-checkbox-btn>
            </template>
          </v-data-table>
        </div>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script>
import ApiService from '@/services/api.service';
import Swal from 'sweetalert2';
import { VDataTable } from 'vuetify/labs/VDataTable';

export default {
  components: { VDataTable },
  data() {
    return {
      error: '',
      container_id: '',
      itemsPerPage: 10,
      items: [],
      headers: [
        { title: 'Package No', align: 'start', key: 'BatchNo' },
        { title: 'Outbound Date', align: 'end', key: 'outgoing_date' },
        { title: 'Action', align: 'end', key: 'is_selected' },
      ],
    };
  },
  computed: {
    computed_outbound_model() {
      return this.items.filter((x) => x.is_selected == true);
    },
  },
  mounted() {
    this.getUnassignPackages();
  },
  methods: {
    getUnassignPackages() {
      ApiService._get('outbound/get-empty-container-result')
        .then(async (response) => {
          if (response.status == 200) {
            let result = await response.json();
            this.items = result;
            this.items.forEach((x) => (x.is_selected = false));
          } else {
            this.networkError();
          }
        })
        .catch(() => {
          this.networkError();
        });
    },
    assign() {
      if (this.computed_outbound_model.length > 0 && this.container_id != '') {
        ApiService._post('outbound/update-container-id', {
          container_id: this.container_id,
          packages: this.computed_outbound_model,
        })
          .then(async (response) => {
            if (response.status == 200) {
              Swal.fire({
                icon: 'success',
                title: 'Container Id assign successfully.',
              });
              this.getUnassignPackages();
            } else {
              Swal.fire({
                icon: 'error',
                title: 'Container Id assign error.',
                text: 'Contact Administrator! Please input valid container id.',
              });
            }
          })
          .catch(() => {
            this.networkError();
          });
      } else if (this.computed_outbound_model.length == 0) {
        Swal.fire({
          icon: 'warning',
          title: 'Please select at least one package number.',
        });
      } else if (this.container_id == '') {
        Swal.fire({
          icon: 'warning',
          title: 'Please input container Id.',
        });
      }
    },
    timeout(ms) {
      return new Promise((resolve) => {
        window.setTimeout(resolve, ms);
      });
    },
    networkError() {
      Swal.fire({
        icon: 'error',
        title: 'Network Error. Could not connect to api service.',
        text: 'Contact Administrator!',
      });
    },
    dateFormat(content) {
      return new Date(content).toString().replace(' GMT+0800 (Malaysia Time)','');
    },
  },
};
</script>
<style scoped>
.scan-confirmation {
  position: absolute;
  width: 100%;
  height: 100%;

  background-color: rgba(255, 255, 255, 0.8);

  display: flex;
  flex-flow: row nowrap;
  justify-content: center;
}

.min-width {
  min-width: 800px;
  margin-top: 50px;
  margin: auto;
}
</style>
