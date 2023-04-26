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
          <v-btn
            variant="elevated"
            @click="outbound"
            >Outbound</v-btn
          >
          <v-data-table
            show-select
            v-model:items-per-page="itemsPerPage"
            :headers="headers"
            :items="items"
            item-value="name"
            class="elevation-1"
          ></v-data-table>
        </div>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script>
import { QrcodeStream } from 'vue3-qrcode-reader';
import ApiService from '@/services/api.service';
import Swal from 'sweetalert2';

export default {
  components: { QrcodeStream },
  data() {
    return {
      error: '',
      decoded_batch_no: '',
      torch: false,
      fetchData: '',
      camera: 'auto',
      hide_scanner: 0,
      scanned_items: [],
      container_id: '',
      itemsPerPage: 20,
      headers: [
        { title: 'Package No', align: 'start', key: 'batch_no' },
        { title: 'Outbound Date', align: 'end', key: 'outbound_date' },
      ],
    };
  },
  computed: {
    computed_outbound_model() {
      let model = {
        ContainerId: this.container_id,
        PackageReferenceNo: this.scanned_items,
      };
      return model;
    },
  },
  methods: {
    addToTable() {
      if (
        this.decoded_batch_no != '' &&
        this.scanned_items.indexOf(this.decoded_batch_no) == -1
      )
        this.scanned_items.push(this.decoded_batch_no);
      this.decoded_batch_no = '';
    },
    outbound() {
      if (this.scanned_items.length > 0) {
        ApiService._post('outbound', this.computed_outbound_model)
          .then(async (response) => {
            let result = await response.json();
            if (response.status == 200) {
              let result_text = '';
              if (result.NotExistsReferenceNo.length > 0)
                result_text += `Batch No not exists : ${result.NotExistsReferenceNo.toString()} \n`;
              if (result.OutboundedReferenceNo.length > 0)
                result_text += `Batch No has been outbound : ${result.OutboundedReferenceNo.toString()} \n`;
              Swal.fire({
                icon: 'success',
                title: 'Outbound success.',
                text: result_text == '' ? `` : result_text,
              });
              this.scanned_items = [];
              this.container_id = '';
            } else {
              Swal.fire({
                icon: 'error',
                title: 'Outbound Error.',
                text: 'Contact Administrator! Outbound process unavailable.',
              });
            }
          })
          .catch(() => {
            Swal.fire({
              icon: 'error',
              title: 'Network Error. Could not connect to api service.',
              text: 'Contact Administrator!',
            });
          });
      } else {
        Swal.fire({
          icon: 'warning',
          title: 'Packages reference number cannot be empty',
        });
      }
    },
    remove(ref_no) {
      let index = this.scanned_items.indexOf(ref_no);
      if (index > -1) {
        // only splice array when item is found
        this.scanned_items.splice(index, 1); // 2nd parameter means remove one item only
      }
    },
    async reloadPage() {
      await this.timeout(500);
      window.location.reload();
    },
    timeout(ms) {
      return new Promise((resolve) => {
        window.setTimeout(resolve, ms);
      });
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
