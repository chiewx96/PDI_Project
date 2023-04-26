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
            class="col-lg-3 col-md-6 col-sm-12"
            required
            hide-details="auto"
            v-model="decoded_batch_no"
            label="Package No"
            prepend-icon="mdi-qrcode"
            @keydown.enter.prevent="addToTable"
          ></v-text-field>
          <v-checkbox
            v-model="hide_scanner"
            value="1"
            false-value="0"
            label="Hide Camera"
          >
          </v-checkbox>
          <qrcode-stream
            v-show="hide_scanner == 0"
            @init="onInit"
            @decode="onDecode"
            :torch="torch"
            :camera="camera"
          ></qrcode-stream>
          <v-text-field
            lg="4"
            md="4"
            sm="12"
            required
            hide-details="auto"
            v-model="container_id"
            label="Container Id"
            prepend-icon="mdi-square-rounded-badge-outline"
          ></v-text-field>

          <v-table
            fixed-header
            height="300px"
          >
            <thead>
              <tr>
                <th class="text-center">Package Number</th>
                <th class="text-center">Action</th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="item in scanned_items"
                :key="item"
              >
                <td>{{ item }}</td>
                <td>
                  <v-btn
                    icon="mdi-delete-outline"
                    @click="remove(item)"
                  ></v-btn>
                </td>
              </tr>
            </tbody>
          </v-table>
          <v-btn
            variant="elevated"
            @click="outbound"
            >Outbound</v-btn
          >
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
    async onInit(promise) {
      try {
        await promise;
      } catch (error) {
        if (error.name === 'NotAllowedError') {
          this.error = 'ERROR: you need to grant camera access permission';
        } else if (error.name === 'NotFoundError') {
          this.error = 'ERROR: no camera on this device';
        } else if (error.name === 'NotSupportedError') {
          this.error = 'ERROR: secure context required (HTTPS, localhost)';
        } else if (error.name === 'NotReadableError') {
          this.error = 'ERROR: is the camera already in use?';
        } else if (error.name === 'OverconstrainedError') {
          this.error = 'ERROR: installed cameras are not suitable';
        } else if (error.name === 'StreamApiNotSupportedError') {
          this.error = 'ERROR: Stream API is not supported in this browser';
        } else if (error.name === 'InsecureContextError') {
          this.error =
            'ERROR: Camera access is only permitted in secure context. Use HTTPS or localhost rather than HTTP.';
        } else {
          this.error = `ERROR: Camera error (${error.name})`;
        }
      } finally {
        this.showScanConfirmation = this.camera === 'off';
      }
    },
    async onDecode(result) {
      this.addToTable();
      // this.decoded_batch_no = result;
      // this.outbound();
    },
    // async showBundleDetails() {
    //   let tableContent =
    //     '<v-dialog v-model="dialog" activator="parent" width="auto">' +
    //     "<v-card>" +
    //     "<v-container>" +
    //     "<v-table>" +
    //     "<thead>" +
    //     "<tr>" +
    //     '<th class="text-left">Reference No</th>' +
    //     '<th class="text-left">Gross Weight</th>' +
    //     '<th class="text-left">Tare Weight</th>' +
    //     '<th class="text-left">Nett Weight</th>' +
    //     '<th class="text-left">Incoming Date Time</th>' +
    //     '<th class="text-left">Created At</th>' +
    //     "</tr>" +
    //     "</thead>" +
    //     "<tbody>" +
    //     "<tr>" +
    //     `<td>${this.fetchData.BatchNo}</td>` +
    //     `<td>${this.fetchData.GrossWeight}</td>` +
    //     `<td>${this.fetchData.TareWeight}</td>` +
    //     `<td>${this.fetchData.NettWeight}</td>` +
    //     `<td>${this.fetchData.IncomingDateTime}</td>` +
    //     `<td>${this.fetchData.CreatedAt}</td>` +
    //     "</tr>" +
    //     "</tbody>" +
    //     "</v-table>" +
    //     "</v-container>" +
    //     "</v-card>";
    //   Swal.fire({
    //     title: "Bundle Information",
    //     html: tableContent,
    //     showCloseButton: true,
    //     showCancelButton: true,
    //     allowOutsideClick: false,
    //     focusConfirm: false,
    //     confirmButtonText: '<i class="fa fa-check"></i> Confirm',
    //     cancelButtonText: '<i class="fa fa-times"></i> Cancel',
    //   }).then((result) => {
    //     if (result.isConfirmed) {
    //       Swal.fire(
    //         "Outbound!",
    //         "This item is registered for outbound.",
    //         "success"
    //       );
    //       result.dismiss === Swal.DismissReason.close;
    //     } else {
    //       result.dismiss === Swal.DismissReason.cancel;
    //     }
    //     this.reloadPage();
    //   });
    // },

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
              this.container_id = "";
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
